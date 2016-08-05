/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp");
var del = require("del");
var project = require("./project.json");
var uglify = require("gulp-uglify");
var concat = require("gulp-concat");
var cssmin = require("gulp-cssmin");
var sass = require("gulp-sass");
var ts = require("gulp-typescript");
var sourcemaps = require("gulp-sourcemaps");
var open = require("gulp-open");
var KarmaServer = require("karma").Server;
var remapIstanbul = require("remap-istanbul/lib/gulpRemapIstanbul");
var environments = require("gulp-environments");
var bump = require("gulp-bump");

var development = environments.development;
var staging = environments.make("staging");
var production = environments.production;

var setEnvironment = function () {
    var environment = process.env.ASPNET_ENV || "Development";
    environment = environment.toLowerCase();
    console.log("Environment:" + environment);
    switch (environment) {
        case "staging":
            environments.current(staging);
            break;
        case "production":
            environments.current(production);
            break;
        default:
            environments.current(development);
            break;
    }
    return true;
};

var paths = {
    webroot: "./wwwroot/",
    libTarget: "./wwwroot/lib/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.sass = "Client/sass/**/*.scss";
paths.fonts = paths.webroot + "fonts";

paths.ts = "Client/typescript/**/*.ts";
paths.ts_no_spec = "Client/typescript/**/!(*spec).ts";
paths.ts_only_spec = "Client/typescript/**/*.spec.ts";

paths.compiledTs = paths.webroot + "app";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";
paths.css = paths.webroot + "css";

paths.npmSrc = "./node_modules/";
paths.bowerSrc = "./bower_components/";

paths.fontAwesome = paths.bowerSrc + "./uikit/fonts/*.*";

paths.lib = [
    paths.bowerSrc + "/jquery/dist/jquery.min.js",
];

paths.angular2 = paths.angular2 = [
    paths.npmSrc + "/@angular/**/*.js",
    paths.npmSrc + "/es6-shim/es6-shim.min.js",
    paths.npmSrc + "/zone.js/dist/zone.js",
    paths.npmSrc + "/reflect-metadata/Reflect.js",
    paths.npmSrc + "/systemjs/dist/system.src.js",
    paths.npmSrc + "/core-js/client/shim.min.js"
];

paths.rxjs = [
    paths.npmSrc + "/rxjs/Subject.js",
    paths.npmSrc + "/rxjs/Observable.js",
    paths.npmSrc + "/rxjs/operator/toPromise.js",
    paths.npmSrc + "/rxjs/observable/fromPromise.js"
];
paths.rxjsall = paths.npmSrc + "/rxjs/**/*.js";

paths.pace = [
    paths.bowerSrc + "/PACE/pace.min.js"
];

paths.symbolObservable = [
    paths.npmSrc + "/symbol-observable/**/*.js"
];

var tsProject = ts.createProject("Client/typescript/tsconfig.json");

gulp.task("clean:ts", function (done) {
    del(paths.compiledTs + "/**/*.*", done);
});

gulp.task("clean:css", function (cb) {
    del(paths.css + "/**/*.*", cb);
});

gulp.task("clean", ["clean:ts", "clean:css"]);

gulp.task("bump", function () {
    gulp.src("./project.json")
    .pipe(bump())
    .pipe(gulp.dest("./"));
});

gulp.task("default",
    setEnvironment() && staging()
    ? ["clean", "lib", "sass", "ts-compile", "bump", "karma-run"]
    : ["clean", "lib", "sass", "ts-compile", "bump"],
    function () {
        return;
    }
);

gulp.task("fonts", function () {
    return gulp.src(paths.fontAwesome)
    .pipe(gulp.dest(paths.fonts));
});

gulp.task("sass", ["fonts"], function () {
    return gulp.src(paths.sass)
        .pipe(sass())
        .pipe(concat(development() ? "style.css" : "style.css"))
        .pipe(staging(cssmin()))
        .pipe(production(cssmin()))
        .pipe(gulp.dest(paths.css));
});

gulp.task("ts-compile", function () {
    var chain = gulp.src(production() ? paths.ts_no_spec : paths.ts);

    if (development() || staging()) {
        chain = chain.pipe(sourcemaps.init());
    }

    chain = chain.pipe(ts(tsProject));

    if (development() || staging()) {
        chain = chain.js
            .pipe(sourcemaps.write(".", { sourceRoot: __dirname + "\\Client\\typescript" }));
    }

    return chain
        .pipe(production(uglify()))
        .pipe(gulp.dest(paths.compiledTs));
});

gulp.task("angular2", function () {
    return gulp.src(paths.angular2).pipe(gulp.dest(paths.libTarget + "/@angular"));
});

gulp.task("rxjs", function () {
    return gulp.src(paths.rxjsall).pipe(gulp.dest(paths.libTarget + "/rxjs"));
});

gulp.task("pace", function () {
    return gulp.src(paths.pace).pipe(gulp.dest(paths.libTarget + "/pace"));
});

gulp.task("symbol-observable", function () {
    return gulp.src(paths.symbolObservable).pipe(gulp.dest(paths.libTarget + "/symbol-observable"));
});

gulp.task("lib", ["angular2", "rxjs", "pace", "symbol-observable"], function () {
    return gulp.src(paths.lib).pipe(gulp.dest(paths.libTarget));
});

gulp.task("karma-run", ["ts-compile"], function (done) {
    new KarmaServer({
        configFile: __dirname + "/karma.conf.js",
        singleRun: true
    }, done).start();
});

gulp.task("karma-run-debug", ["ts-compile"], function (done) {
    new KarmaServer({
        configFile: __dirname + "/karma.conf.js",
        singleRun: false
    }, done).start();
});

gulp.task("karma-prepare-reports", ["karma-run"], function (done) {
    return gulp.src(__dirname + "/Client/tests_report/coverage/coverage-final.json")
        .pipe(remapIstanbul({
            reports: {
                'html': __dirname + "/Client/tests_report/coverage_html"
            }
        }));
});

gulp.task("karma-reports", ["karma-prepare-reports"], function (done) {
    gulp.src("./Client/tests_report/coverage_html/index.html")
        .pipe(open());
    gulp.src("./Client/tests_report/jasmine_html/report/index.html")
        .pipe(open());
    done();
});

gulp.task("watch", ["ts-compile", "sass"], function () {
    gulp.watch(paths.ts_no_spec, ["ts-compile"]);
    gulp.watch(paths.ts_only_spec, ["ts-compile"]);
    gulp.watch(paths.sass, ["sass"]);
});