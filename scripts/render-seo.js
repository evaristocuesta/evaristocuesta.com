'use strict';
const fs = require('fs');
const upath = require('upath');
const sh = require('shelljs');

module.exports = function renderSeo() {
    const sourcePath = upath.resolve(upath.dirname(__filename), '../src/seo/*');
    const destPath = upath.resolve(upath.dirname(__filename), '../dist/');
    
    sh.cp('-R', sourcePath, destPath)
};