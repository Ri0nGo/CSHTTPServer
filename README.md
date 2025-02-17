# opcda-collector

使用原生的Net包构建的一个Http Server 程序


# 功能


- [X] 封装了请求和响应的处理方法（仅针对Content-Type=application/json请求）
- [ ] 基于HTTPMethod 构建路由树
- [ ] 提供GET，POST，PUT，DELETE，PATCH等HTTP 方法的快速注册
- [ ] 多线程处理 HTTP 请求




## MSBuild

```
# 适配 Windows7 运行
msbuild opcda-collector.csproj /p:Configuration=Release /p:RuntimeIdentifier=win-x86 /p:Platform=x86 /p:SelfContained=true /p:PublishSingleFile=true /t:Publish
```

## 注意

项目仅用于本人学习，代码未做详细测试。
