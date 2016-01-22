$baseDir = Resolve-Path ..

Get-Childitem $baseDir -Include bin, obj -Recurse | 
Where {$_.psIsContainer -eq $true} | 
Foreach-Object { 
	Write-Host "deleting" $_.fullname
	Remove-Item $_.fullname -Force -Recurse -ErrorAction SilentlyContinue
}