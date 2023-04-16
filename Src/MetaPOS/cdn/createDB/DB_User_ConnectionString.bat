set newdb=%1 
set newuser=%2
set newuserpassword=%3

set email=%newuser%@robiamarhishab.xyz
set url=%newdb%.robiamarhishab.xyz


sqlcmd -S CQJW-A71767F9F\SQLEXPRESS -U metapos2020 -P JdmIemj3(#21323234Kds -i .\NDB_NUSR_NUSRPASS.sql -v NDB= %newdb% NUSR= %newuser% NUSRPASS= %newuserpassword% EMAIL=%email%

powershell -File .\test_add_connectionString.ps1 -NDB %newdb% -NUSR %newuser% -NUSRPASS %newuserpassword% -URL %url%
