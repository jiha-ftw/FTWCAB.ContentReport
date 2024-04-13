@echo off

cd frontend
call npm run build
cd ..

copy /y frontend\build\static\css\main*.css ClientResources\main.css
copy /y frontend\build\static\js\main*.js ClientResources\main.js