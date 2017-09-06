cd Android.tooltips\
.\gradlew tooltips:clean tooltips:assembleRelease -x lint --stacktrace
cp tooltips\build\outputs\aar\tooltips-release.aar ..\Xamarin.Android.Tooltips\Jars\
cd ..\
.\build.ps1 -v diagnostic 