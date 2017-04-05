var gulp = require('gulp');
var loadJsonFile = require('load-json-file');
var sass = require('gulp-sass');
var sassLint = require('gulp-sass-lint');
//var concat = require('gulp-concat');
var autoprefixer = require('gulp-autoprefixer');
var cleanCss = require('gulp-clean-css');
var sourcemaps = require('gulp-sourcemaps');
var config = loadJsonFile.sync('gulpconfig.json');
var scssInput = config.paths.stylesheets.scss;
var cssOutput = config.paths.stylesheets.css;

var sassOptions = {
    errLogToConsole: true
};

gulp.task('dev-css', () => {
    return gulp
        .src(scssInput)
        .pipe(sassLint())
        .pipe(sassLint.format())
        .pipe(sassLint.failOnError())
        .pipe(sourcemaps.init())
        .pipe(sass(sassOptions).on('error', sass.logError))
        .pipe(autoprefixer())
        .pipe(sourcemaps.write())
        .pipe(gulp.dest(cssOutput));
});

gulp.task('pub-css', () => {
    return gulp
        .src(scssInput)
        .pipe(sass(sassOptions).on('error', sass.logError))
        .pipe(autoprefixer())
        .pipe(cleanCss())
        .pipe(gulp.dest(cssOutput));
});

gulp.task('watch', () => {
    return gulp
        .watch(scssInput, ['dev-css'])
        .on('change', (event) => {
            console.log('File ' + event.path + ' was ' + event.type + ', running tasks...');
        });
});

gulp.task('default', ['dev-css', 'watch']);
