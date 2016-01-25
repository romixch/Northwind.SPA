module.exports = function(config) {
  config.set({
    browsers: ['Chrome'],
    frameworks: ['jasmine'],
    files: ['specs/*.js'],
    reporters: ['html']
  });
};