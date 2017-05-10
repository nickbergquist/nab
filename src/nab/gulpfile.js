'use strict';

const gulp = require('gulp');
const util = require('gulp-util');
const pathExists = require('path-exists');
const rename = require('gulp-rename');
const loadJsonFile = require('load-json-file');
const sass = require('gulp-sass');
const sassLint = require('gulp-sass-lint');
const autoprefixer = require('gulp-autoprefixer');
const cleanCss = require('gulp-clean-css');
const sourcemaps = require('gulp-sourcemaps');
const config = loadJsonFile.sync('gulpconfig.json');

const scssInput = config.paths.stylesheets.scss;
const scssIgnore = config.paths.stylesheets.ignore;
const scssThemes = config.paths.stylesheets.themes;
const cssOutput = config.paths.stylesheets.css;

const sassOptions = {
    errLogToConsole: true
};

gulp.task('dev-css', () => {
    return gulp
        .src([scssInput, scssIgnore])
        .pipe(sassLint())
        .pipe(sassLint.format())
        .pipe(sassLint.failOnError())
        .pipe(sourcemaps.init())
        .pipe(sass(sassOptions).on('error', sass.logError))
        .pipe(autoprefixer())
        .pipe(sourcemaps.write().on('end', () => util.log('Sourcemap created')))
        .pipe(gulp.dest(cssOutput).on('end', () => util.log('CSS written to ' + cssOutput)));
});

gulp.task('pub-css', () => {
    return gulp
        .src([scssInput, scssIgnore])
        .pipe(sass(sassOptions).on('error', sass.logError))
        .pipe(autoprefixer())
        .pipe(cleanCss().on('end', () => util.log('CSS minified')))
        .pipe(gulp.dest(cssOutput).on('end', () => util.log('CSS written to ' + cssOutput)));
});

// usage: gulp theme --name name-of-theme
gulp.task('theme', () => {
	let themePath = scssThemes + util.env.name + '.scss';
	let filePresent = pathExists.sync(themePath);

    if (filePresent) {
        util.log('File at ' + themePath + ' exists, starting pipe...');
        return gulp
            .src(themePath)
            .pipe(sass(sassOptions).on('error', sass.logError))
            .pipe(autoprefixer())
            //.pipe(cleanCss().on('end', () => util.log('CSS minified')))
            .pipe(rename('main.css'))
            .pipe(gulp.dest(cssOutput).on('end', () => util.log('CSS written to ' + cssOutput)));
    } else {
        util.log('File at ' + themePath + ' missing, check the SASS theme name matches the task name parameter.');
        util.log('Usage: gulp theme --name name-of-theme');
        util.log('Exiting process');
        process.exit();
    }
});

gulp.task('watch', () => {
    return gulp
        .watch([scssInput, scssIgnore], ['dev-css'])
        .on('change', (event) => {
            console.log('File ' + event.path + ' was ' + event.type + ', running tasks...');
        });
});

gulp.task('default', ['dev-css', 'watch']);
