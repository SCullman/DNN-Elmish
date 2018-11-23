# A Template for DNN Modules build with F#

This template can be used to generate a simple web app with [Fable](http://fable.io/).
You can find more templates by searching `Fable.Template` packages in [Nuget](https://www.nuget.org).

## Requirements

* [dotnet Framework](https://www.microsoft.com/net/download/) 4.6 or higher
* [node.js](https://nodejs.org) 6.11 or higher
* A JS package manager: [yarn](https://yarnpkg.com) or [npm](http://npmjs.com/)


> npm comes bundled with node.js, but we recommend to use at least npm 5. If you have npm installed, you can upgrade it by running `npm install -g npm`.


## Editor

The project can be used by editors compatible with the new .fsproj format, like VS Code + [Ionide](http://ionide.io/), Emacs with [fsharp-mode](https://github.com/fsharp/emacs-fsharp-mode) or [Rider](https://www.jetbrains.com/rider/). 

## Building and running the client 

> In the commands below, yarn is the tool of choice. If you want to use npm, just replace `yarn` by `npm` in the commands.

* Install JS dependencies: `yarn install`
* Install F# dependencies: `.paket\paket intall`
* **Move to project folder**: `cd src/client`
* Start Fable daemon and [Webpack](https://webpack.js.org/) dev server: `dotnet fable yarn-start`
* In your browser, open: http://localhost:8080/

> `dotnet fable yarn-start` (or `npm-start`) is used to start the Fable daemon and run a script in package.json concurrently. It's a shortcut of `yarn-run [SCRIPT_NAME]`, e.g. `dotnet fable yarn-run start`.

Any modification you do to the F# code will be reflected in the web page after saving. When you want to output the JS code to disk, run `dotnet fable yarn-build` and you'll get a minified JS bundle in the `public` folder.

## Building and running the service

ToDo: Discription missing

## Create a module package

ToDo: Discription missing

## Project structure

### Paket

[Paket](https://fsprojects.github.io/Paket/) is the package manager used for F# dependencies. It doesn't need a global installation, the binary is included in the `.paket` folder. Other Paket related files are:

- **paket.dependencies**: contains all the dependencies in the repository.
- **paket.references**: there should be one such a file next to each `.fsproj` file.
- **paket.lock**: automatically generated, but should be committed to source control, [see why](https://fsprojects.github.io/Paket/faq.html#Why-should-I-commit-the-lock-file).
- **Nuget.Config**: prevents conflicts with Paket in machines with some Nuget configuration.

> Paket dependencies will be installed in the `packages` directory. See [Paket website](https://fsprojects.github.io/Paket/) for more info.

### yarn/npm

- **package.json**: contains the JS dependencies together with other info, like development scripts.
- **yarn.lock**: is the lock file created by yarn.
- **package-lock.json**: is the lock file understood by npm 5, if you use it instead of yarn.

> JS dependencies will be installed in `node_modules`. See [yarn](https://yarnpkg.com) and/or [npm](http://npmjs.com/) websites for more info.

### Webpack

[Webpack](https://webpack.js.org) is a bundler, which links different JS sources into a single file making deployment much easier. It also offers other features, like a static dev server that can automatically refresh the browser upon changes in your code or a minifier for production release. Fable interacts with Webpack through the `fable-loader`.

- **webpack.config.js**: is the configuration file for Webpack. It allows you to set many things: like the path of the bundle, the port for the development server or [Babel](https://babeljs.io/) options. See [Webpack website](https://webpack.js.org) for more info.

