let appInjectorRef;

export const appInjector = (injector?: any) => {
    if (!injector || injector == null) {
        return appInjectorRef;
    }

    appInjectorRef = injector;

    return appInjectorRef;
};