# opcda-collector

使用原生的Net包构建的一个Http Server 程序


## MSBuild

```
# 适配 Windows7 运行
msbuild opcda-collector.csproj /p:Configuration=Release /p:RuntimeIdentifier=win-x86 /p:Platform=x86 /p:SelfContained=true /p:PublishSingleFile=true /t:Publish
```

## 注意

项目仅用于本人学习，代码未做详细测试。