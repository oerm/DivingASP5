﻿/// <binding ProjectOpened='bower' />
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
                     path + "bootstrap/js/modal.js",
                     path + "moment/min/moment.min.js",
                     path + "angular/angular.js",
                     path + "blueimp-load-image/js/load-image.all.min.js",
                     path + "blueimp-canvas-to-blob/js/canvas-to-blob.js",
                     path + "jquery-file-upload/uploader.js",
                     path + "angular-animate/angular-animate.js",
                     path + "eonasdan-bootstrap-datetimepicker/src/js/bootstrap-datetimepicker.js",
                     path + "jasny-bootstrap/js/jasny-bootstrap.js"])
               .pipe(concat('site.min.js'))
               /*
               .pipe(uglify({ mangle: false }))
               */
               .pipe(gulp.dest('wwwroot/js'));    
});

gulp.task('buildangularcontrollers', function () {
    return gulp.src([path + "custom/services/dataprovider.js",
                     path + "custom/controllers/rootcontroller.js",
                     path + "custom/controllers/logincontroller.js",
                     path + "custom/controllers/paspcontroller.js",
                     path + "custom/controllers/divecontroller.js",
                     path + "custom/app.js"])
               .pipe(concat('controllers.min.js'))
                /*
               .pipe(uglify({ mangle: false }))
                */
               .pipe(gulp.dest('wwwroot/js'));
});

gulp.task('buildcss', function () {
    return gulp.src([path + "bootstrap/dist/css/bootstrap.css",
                     path + "jquery-file-upload/css/jquery.fileupload.css",
                     path + "jquery-file-upload/css/jquery.fileupload-ui.css",
                     path + "font-awesome/css/font-awesome.css",
                     path + "jasny-bootstrap/css/jasny-bootstrap.css",
                     path + "eonasdan-bootstrap-datetimepicker/build/css/bootstrap-datetimepicker.css",
                     path + "custom/css/custom.css"])
               .pipe(concat('site.min.css'))
               .pipe(minifycss())
               .pipe(gulp.dest('wwwroot/css'));
});

gulp.task('buildfonts', function () {
    return gulp.src(path + "font-awesome/fonts/*")
               .pipe(gulp.dest('wwwroot/fonts'));
});


gulp.task('default', ['buildjs','buildangularcontrollers', 'buildcss', 'buildfonts'], function () { });