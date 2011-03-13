@ECHO off
ECHO %1 Passed command is the debug/release flag
ECHO Declare paths needed, package path is cleaned at start
set project= "C:\inetpub\wwwroot\DotNetNuke\DesktopModules\flowmarks_Events"
set package= "C:\inetpub\wwwroot\DotNetNuke\DesktopModules\flowmarks_Events\flowmarks_Events_Install"
set resources= "C:\inetpub\wwwroot\DotNetNuke\DesktopModules\flowmarks_Events\flowmarks_Events_Install\resources"

MKDIR %package%
MKDIR %resources%

ECHO Delete Existing Files from package location!  CAREFUL!!!
ECHO Y | DEL %resources%\*.*
ECHO Y | DEL %package%\*.*

ECHO Copy resource files
MKDIR %resources%\App_LocalResources
XCOPY /Y %project%\App_LocalResources\*.resx %resources%\App_LocalResources

REM Copy Images
MKDIR %resources%\images
XCOPY /Y %project%\images\*.* %resources%\images

REM Copy User Controls
XCOPY /Y %project%\*.ascx %resources%

REM Copy CSS
XCOPY /Y %project%\*.css %resources%

REM Copy JS
XCOPY /Y %project%\*.js %resources%


REM Copy DNN File
XCOPY /Y %project%\*.dnn %package%
XCOPY /Y %project%\*.dnn5 %package%

REM Copy Google Gadget
XCOPY /Y %project%\flowmarks_Events.xml %package%


REM Copy Txt
XCOPY /Y %project%\*.txt %package%

REM Copy SqlDataProvider files
XCOPY /Y %project%\*.SqlDataProvider %package%

REM Copy DLL Files, note use of flag to grab debug/release depending on passed value
XCOPY /Y %project%\obj\%1\*.dll %package%

XCOPY /Y %project%\build\*.dll %package%