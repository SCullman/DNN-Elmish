
var path = require("path");
var webpack = require("webpack");
var HtmlWebpackPlugin = require('html-webpack-plugin');
var CopyWebpackPlugin = require('copy-webpack-plugin');
var MiniCssExtractPlugin = require("mini-css-extract-plugin");


function resolve(filePath) {
    return path.join(__dirname, filePath)
}

var babelOptions = {
   presets: [
       ["@babel/preset-env", {
           "targets": {
               "browsers": ["last 2 versions"]
           },
           "modules": false
       }]
   ]
};

// If we're running the webpack-dev-server, assume we're in development mode

var isProduction = !process.argv.find(v => v.indexOf('webpack-dev-server') !== -1);
console.log("Bundling for " + (isProduction ? "production" : "development") + "...");

module.exports = {
    devtool: isProduction ? undefined : "source-map",
    entry: resolve('./src/client/client.fsproj'),
    output: {
        filename: 'bundle.js',
        path: resolve('./public'),
    },
   
    devServer: {
        contentBase: resolve('./public'),
        port: 8080,
        hot: true,
        inline: true
    },
    mode: isProduction ? "production" : "development",
   
    module: {
        rules: [
            {
                test: /\.fs(x|proj)?$/,
                use: {
                    loader: "fable-loader",
                    options: {
                        babel: babelOptions,
                        define: isProduction ? [] : ["DEBUG"]
                    }
                }
            },
            {
                test: /\.js$/,
                exclude: /node_modules/,
                use: {
                    loader: 'babel-loader',
                    options: babelOptions
                },
            },
            {
                test: /\.(sa|c)ss$/,
                use: [
                    "style-loader",
                    "css-loader",
                    "sass-loader"
                ]
            }
        ]
    },

    plugins: isProduction ? [] : [
        new webpack.HotModuleReplacementPlugin(),
        new webpack.NamedModulesPlugin()
    ]
};
