//-------------------------------------------------------------------------------------------------
// requires
//-------------------------------------------------------------------------------------------------
var gulp = require('gulp');
var del = require('del');
var ts = require('gulp-typescript');
var tslint = require('gulp-tslint');
var sourcemaps = require('gulp-sourcemaps');
var less = require('gulp-less');
var babel = require('gulp-babel');

var browserSync = require('browser-sync').create();
var KarmaServer = require('karma').Server;

var $ = require("gulp-load-plugins")({
	pattern: ['gulp-*', 'gulp.*', 'main-bower-files'],
	replaceString: /\bgulp[\-.]/
});

//-------------------------------------------------------------------------------------------------
// globals
//-------------------------------------------------------------------------------------------------
var directories = {
    application : './application/',
    destination : './dist/',
    stles : './styles/',
    specs : './specs/',
    bower : {
        bootstrap : './bower_components/bootstrap/dist/css/'
    }
}

//-------------------------------------------------------------------------------------------------
// tasks
//-------------------------------------------------------------------------------------------------
gulp.task('default', ['html', 'test']);

gulp.task('typescript', function() {
	return gulp.src(directories.application + '*')
		.pipe($.filter(['*.ts', '!*.test.ts']))
        .pipe(tslint())
        .pipe(tslint.report("verbose"))
		.pipe(ts({ noImplicitAny: true, target: "es2015", out: 'application.js' }))
        .pipe(sourcemaps.init())
        .pipe(babel({ presets: ['es2015'] }))
        .pipe($.uglify())
        .pipe(sourcemaps.write('.'))
		.pipe(gulp.dest(directories.destination));
});

gulp.task('js', function() {
	return gulp.src($.mainBowerFiles())
		.pipe($.filter('*.js'))
		.pipe($.concat('main.js'))
        .pipe(sourcemaps.init())
		.pipe($.uglify())
        .pipe(sourcemaps.write('.'))
		.pipe(gulp.dest(directories.destination));
});

gulp.task('less', function() {
    return gulp.src(directories.stles + 'bootstrap.less')
        .pipe(less())
        .pipe(gulp.dest(directories.bower.bootstrap));
})

gulp.task('css', function() {
	return gulp.src($.mainBowerFiles().concat(directories.stles + "*"))
		.pipe($.filter('*.css'))
		.pipe($.concat('main.css'))
        .pipe(sourcemaps.init())
		.pipe($.cssnano())
        .pipe(sourcemaps.write('.'))
		.pipe(gulp.dest(directories.destination));
});

gulp.task('fonts', function() {
    return gulp.src($.mainBowerFiles())
        .pipe($.filter('**/*.{eot,svg,ttf,woff,woff2}'))
        .pipe(gulp.dest(directories.destination + 'fonts/'));
})

gulp.task('html', ['typescript', 'js', 'less', 'css', 'fonts'], function () {
    gulp.src(directories.application + '**/*.html')
	    .pipe(gulp.dest(directories.destination));
        
    gulp.src("index.html")
        .pipe($.inject(gulp.src(['main.css', 'main.js', 'application.js'], { cwd: directories.destination, read: false }), { addRootSlash: false }))
        .pipe(gulp.dest(directories.destination));
});

gulp.task('show', ['html'], function () {
    browserSync.init({ server: directories.destination });
    
    gulp.watch(directories.application + '**/*.ts', ['typescript']).on('change', browserSync.reload);
    gulp.watch([directories.application + '**/*.html', 'index.html'], ['html']).on('change', browserSync.reload);
});

gulp.task('serve', ['html', 'test'], function () {
    browserSync.init({ server: directories.destination });

	new KarmaServer({ configFile: __dirname + '/karma.conf.js', singleRun: false, autoWatch: true }).start();
	
    gulp.watch(directories.application + '**/*.ts', ['typescript', 'test']).on('change', browserSync.reload);
    gulp.watch(directories.application + '**/*.html', ['html']).on('change', browserSync.reload);
});

gulp.task('tdd', ['test'], function() {
	new KarmaServer({ configFile: __dirname + '/karma.conf.js', singleRun: false, autoWatch: true }).start();
	
    gulp.watch(directories.application + '**/*.ts', ['test']);
});

gulp.task('test', function () {
	gulp.src($.mainBowerFiles({ includeDev: true }))
		.pipe($.filter('*.js'))
		.pipe($.concat('main.js'))
		.pipe(gulp.dest(directories.specs));
	
	gulp.src(directories.application + '**/*.test.ts')
		.pipe(ts({ noImplicitAny: true, out: 'specs.js' }))
		.pipe(gulp.dest(directories.specs))
});

gulp.task('clean', function() {
	del(directories.destination);
	del(directories.specs);
});