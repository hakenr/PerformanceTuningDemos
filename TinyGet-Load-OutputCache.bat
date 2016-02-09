echo off
cls
echo Performance counters
echo  ASP.NET Applications \ Requests/s
pause
echo on
tinyget -srv:localhost -port:36234 -uri:/OutputCache.aspx -loop:40000 -threads:10