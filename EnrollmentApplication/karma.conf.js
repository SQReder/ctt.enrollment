module.exports = function (config) {
    config.set({
        basePath: '.',

        frameworks: ['jasmine'],

        files: [
            // paths loaded by Karma
            { pattern: 'bower_components/jquery/dist/jquery.js', included: true, watched: true },
            { pattern: 'bower_components/signalr/jquery.signalR.js', included: true, watched: true },
            { pattern: 'node_modules/alertifyjs/build/alertify.min.js', included: true, watched: true },

            { pattern: 'node_modules/es6-shim/es6-shim.min.js', included: true, watched: true },
            { pattern: 'node_modules/systemjs/dist/system-polyfills.js', included: true, watched: true },
            { pattern: 'node_modules/angular2/es6/dev/src/testing/shims_for_IE.js', included: true, watched: true },
            { pattern: 'node_modules/angular2/bundles/angular2-polyfills.js', included: true, watched: true },
            { pattern: 'node_modules/systemjs/dist/system.src.js', included: true, watched: true },
            { pattern: 'node_modules/rxjs/bundles/Rx.js', included: true, watched: true },
            { pattern: 'node_modules/angular2/bundles/angular2.dev.js', included: true, watched: true },
            { pattern: 'node_modules/angular2/bundles/testing.dev.js', included: true, watched: true },
            { pattern: 'node_modules/angular2/bundles/router.dev.js', included: true, watched: true },
            { pattern: 'node_modules/angular2/bundles/http.js', included: true, watched: true },

            { pattern: 'node_modules/jasmine-ajax/lib/mock-ajax.js', included: true, watched: true },

            { pattern: 'karma-test-shim.js', included: true, watched: true },

            // paths loaded via module imports
            { pattern: 'wwwroot/app/**/*.js', included: false, watched: true },

            // paths to support debugging with source maps in dev tools
            { pattern: 'Client/typescript/**/*.ts', included: false, watched: false },
            { pattern: 'wwwroot/app/**/*.js.map', included: false, watched: false }
        ],

        // proxied base paths
        proxies: {
            // required for component assests fetched by Angular's compiler
            '/src/': '/base/wwwroot/app/'
        },

        port: 9876,

        //logLevel: config.LOG_INFO,

        colors: true,

        autoWatch: true,

        browsers: ['Chrome'],

        // Karma plugins loaded
        plugins: [
            'karma-jasmine',
            'karma-coverage',
            'karma-chrome-launcher',
            'karma-html-reporter',
            'karma-jasmine-html-reporter',
            "karma-junit-reporter"
        ],

        reporters: ["coverage", "html", "junit", "kjhtml"],

        // Source files that you wanna generate coverage for.
        // Do not include tests or libraries (these files will be instrumented by Istanbul)
        preprocessors: {
            'wwwroot/app/**/!(*spec).js': ['coverage']
        },

        junitReporter : {
            outputFile: 'Client/tests_report/jenkins/test-results.xml',
            useBrowserName: false
        },

        coverageReporter: {
            dir: 'Client/tests_report/coverage',
            reporters: [
                { type: 'json', subdir: '.', file: 'coverage-final.json' },
                { type: 'cobertura', subdir: '.', file: 'coverage-jenkins.xml' }
            ]
        },
        
        specReporter: {
            maxLogLines: 5,         // limit number of lines logged per test
            suppressErrorSummary: false,  // do not print error summary
            suppressFailed: false,  // do not print information about failed tests
            suppressPassed: false,  // do not print information about passed tests
            suppressSkipped: false  // do not print information about skipped tests
        },

        htmlReporter: {
            outputDir: 'Client/tests_report/jasmine_html', // where to put the reports  
            templatePath: null, // set if you moved jasmine_template.html 
            focusOnFailures: true, // reports show failures on start 
            namedFiles: false, // name files instead of creating sub-directories 
            pageTitle: "Jasmine report", // page title for reports; browser info by default 
            urlFriendlyName: false, // simply replaces spaces with _ for files/dirs 
            reportName: 'report', // report summary filename; browser info by default 


            // experimental 
            preserveDescribeNesting: false, // folded suites stay folded  
            foldAll: false // reports start folded (only with preserveDescribeNesting) 
        },

        singleRun: true
    });
};