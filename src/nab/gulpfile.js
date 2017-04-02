var gulp = require('gulp');
var loadJsonFile = require('load-json-file');
var sass = require('gulp-sass');
var sassLint = require('gulp-sass-lint');
var concat = require('gulp-concat');
var autoprefixer = require('gulp-autoprefixer');
var sourcemaps = require('gulp-sourcemaps');
var config = loadJsonFile.sync('gulpconfig.json');
var scssInput = config.paths.stylesheets.scss;
var cssOutput = config.paths.stylesheets.css;

var sassOptions = {
    errLogToConsole: true,
    outputStyle: 'compressed' //expanded
};

gulp.task('pub-css', () => {
    return gulp
        .src(scssInput)
        .pipe(sassLint())
        .pipe(sassLint.format())
        .pipe(sassLint.failOnError())
        .pipe(sourcemaps.init())
        .pipe(sass(sassOptions).on('error', sass.logError))
        .pipe(sourcemaps.write())
        .pipe(autoprefixer())
        .pipe(concat('site.css'))
        .pipe(gulp.dest(cssOutput));
});

gulp.task('watch', function () {
    return gulp
        .watch(scssInput, ['pub-css'])
        .on('change', function (event) {
            console.log('File ' + event.path + ' was ' + event.type + ', running tasks...');
        });
});

gulp.task('default', ['pub-css', 'watch']);
