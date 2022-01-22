'use strict';
const fs = require('fs');
const upath = require('upath');
const path = require('path');
const pug = require('pug');
const sh = require('shelljs');
const prettier = require('prettier');
const webConfigJSON = require('../src/config/webconfig.json');

module.exports = function renderPug(filePath) {
    const destPath = filePath.replace(/src\/pug\//, 'dist/').replace(/\.pug$/, '.html');
    const srcPath = upath.resolve(upath.dirname(__filename), '../src');
    var filename = path.basename(destPath, '.html');
    console.log(`### INFO: Rendering ${filePath} to ${destPath} - ${filename}`);
    const html = pug.renderFile(filePath, {
        doctype: 'html',
        filename: filePath,
        basedir: srcPath, 
        title: webConfigJSON[filename].title, 
        description: webConfigJSON[filename].description
    });

    const destPathDirname = upath.dirname(destPath);
    if (!sh.test('-e', destPathDirname)) {
        sh.mkdir('-p', destPathDirname);
    }

    const prettified = prettier.format(html, {
        printWidth: 1000,
        tabWidth: 4,
        singleQuote: true,
        proseWrap: 'preserve',
        endOfLine: 'lf',
        parser: 'html',
        htmlWhitespaceSensitivity: 'ignore'
    });

    fs.writeFileSync(destPath, prettified);
};
