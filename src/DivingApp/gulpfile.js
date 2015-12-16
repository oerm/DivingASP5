/// <binding ProjectOpened='bower' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp'),
    minifycss = require('gulp-minify-css'),
    uglify = require('gulp-uglify'),
    rename = require('gulp-rename'),
    concat = require('gulp-concat');

var path = "wwwroot/lib/";

gulp.task('buildjs', function () {
    return gulp.src([path + "jquery/dist/jquery.js",                     
                     path + "bootstrap/dist/js/bootstrap.js",
                     path + "moment/min/moment.min.js",
                     path + "angular/angular.js",
                     path + "eonasdan-bootstrap-datetimepicker/src/js/bootstrap-datetimepicker.js",
                     path + "jasny-bootstrap/js/jasny-bootstrap.js"])
               .pipe(concat('site.min.js'))

               .pipe(gulp.dest('wwwroot/js'));    
});

gulp.task('buildangularcontrollers', function () {
    return gulp.src([path + "angular/controllers/logincontroller.js"])
               .pipe(concat('controllers.min.js'))
               .pipe(uglify({ mangle: false }))
               .pipe(gulp.dest('wwwroot/js'));
});

gulp.task('buildcss', function () {
    return gulp.src([path + "bootstrap/dist/css/bootstrap.css",
                     
                     path + "font-awesome/css/font-awesome.css",
                     path + "jasny-bootstrap/css/jasny-bootstrap.css",
                     path + "eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.css",
                     path + "custom/custom.css"])
               .pipe(concat('site.min.css'))
               .pipe(minifycss())
               .pipe(gulp.dest('wwwroot/css'));
});

gulp.task('buildfonts', function () {
    return gulp.src(path + "font-awesome/fonts/*")
               .pipe(gulp.dest('wwwroot/fonts'));
});


gulp.task('default', ['buildjs','buildangularcontrollers', 'buildcss', 'buildfonts'], function () { });