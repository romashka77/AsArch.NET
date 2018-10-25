var path = require('path');

module.exports = {
    entry: {
        tabdopisk: "./Scripts/components/tab-dop-isk.jsx"
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
            }
        ]
    }
}