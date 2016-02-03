param([string] $environment)

function ExecuteMeasuredCommand($cmd)
{
	Write-Host ("Executing {0}..." -f $cmd) -ForegroundColor "magenta"
	$timeElapsed = Measure-Command { &$cmd | Out-Host }
	Write-Host ("...done. Took {0} seconds to complete." -f $timeElapsed.TotalSeconds) -ForegroundColor "magenta"
	Write-Host ""
}

ExecuteMeasuredCommand {npm install --silent}
ExecuteMeasuredCommand {bower install}
ExecuteMeasuredCommand {tsd install}
ExecuteMeasuredCommand {gulp --env $environment}