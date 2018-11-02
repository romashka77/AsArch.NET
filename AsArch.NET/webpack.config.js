var path = require('path');

module.exports = {
    entry: {
        iskovoezajvlenie: "./Scripts/components/IskovoeZajvlenie.jsx"
        //"./src/app.jsx"
    }, // входная точка - исходный файл
    output: {
        path: path.resolve(__dirname, './Scripts'),     // путь к каталогу выходных файлов - папка public
        publicPath: '/Scripts/',
        filename: "[name].bundle.js"       // название создаваемого файла
    },
    module: {
        rules: [   //загрузчик для jsx
            {
                test: /\.jsx?$/, // определяем тип файлов
                exclude: /(node_modules)/,  // исключаем из обработки папку node_modules
                loader: "babel-loader",   // определяем загрузчик
                options: {
                    presets: [
                        "@babel/preset-env",
                        "@babel/preset-react",
                        {
                            'plugins': [
                                '@babel/plugin-proposal-class-properties'
                            ]
                        }
                    ]    // используемые плагины
                }
            },
            {
                test: /\.css$/,
                use: [
                    {
                        loader: "style-loader"
                    },
                    {
                        loader: "css-loader",
                        options: {
                            sourceMap: true,
                            modules: true,
                            localIdentName: "[local]___[hash:base64:5]"
                        }
                    },
                    {
                        loader: "less-loader"
                    }
                ]
            }
        ]
    }
}