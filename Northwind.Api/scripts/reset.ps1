$baseDir = Resolve-Path ..
$packagesDir = "$baseDir\packages"
	
& .\clean.ps1	
	
Write-Host "deleting" $packagesDir	
Remove-Item $packagesDir -Force -Recurse -ErrorAction SilentlyContinue	

Write-Host "deleting suo files"	
Remove-Item "..\*.suo" -Force -ErrorAction SilentlyContinue	
	
Write-Host "deleting StyleCop cache files"	
Remove-Item "..\*\StyleCop.Cache" -Force -ErrorAction SilentlyContinue	
