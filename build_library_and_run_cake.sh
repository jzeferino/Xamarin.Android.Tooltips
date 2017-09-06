#!/bin/bash
cd Android.tooltips/
# build release bypassing lint
./gradlew tooltips:clean tooltips:assembleRelease -x lint
cp tooltips/build/outputs/aar/tooltips-release.aar ../Xamarin.Android.Tooltips/Jars/
cd ../
./build.sh