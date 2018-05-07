#!/bin/sh

# 1.安装 ios-deploy: $ sudo npm i -g ios-deploy --unsafe-perm=true
# 2.查看SDK: $ xcodebuild -showsdks

cd ./BuildIOS

if [ $1 == "b" ]
then
    xcodebuild -sdk iphoneos11.3 build
elif [ $1 == "r" ]
then
    ios-deploy -d -I --bundle ./build/Release-iphoneos/ProductName.app
elif [ $1 == "br" ]
then
    xcodebuild -sdk iphoneos11.3 build
    ios-deploy -d -I --bundle ./build/Release-iphoneos/ProductName.app
else
    echo "情输入参数 br=编译+运行 b=编译 r=运行"
fi

