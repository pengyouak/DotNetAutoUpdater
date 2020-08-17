### 自动更新组件

---

#### XML 配置

```
<?xml version="1.0" encoding="utf-8"?>
<UpdateOption xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Version>1.1.1.1</Version>
  <ServerUrl>http://xxx.xxx.xxx/</ServerUrl>
  <UpdateMode>Force</UpdateMode>
  <ChangeLog></ChangeLog>
  <UpdateItems>
    <UpdateItem>
      <UpdateDate>0001-01-01T00:00:00</UpdateDate>
      <MinVersion>0.0.0.0</MinVersion>
      <Version>1.0.0.0</Version>
      <Path>DotNetAutoUpdater.dll</Path>
      <ValideMode>Version</ValideMode>
      <Required>false</Required>
    </UpdateItem>
    </UpdateItem>
    <UpdateItem>
      <UpdateDate>0001-01-01T00:00:00</UpdateDate>
      <MinVersion>0.0.0.0</MinVersion>
      <Version>1.0.0.0</Version>
      <Path>b/DotNetAutoUpdaterTest.exe</Path>
      <ValideMode>Version</ValideMode>
      <Required>false</Required>
    </UpdateItem>
    </UpdateItem>
  </UpdateItems>
</UpdateOption>
```

- Prompt: 提示更新
- PromptAndDetail: 提示并显示更新详情
- Force: 强制更新

#### 使用

```
new AutoUpdate().Update("http://xxx.xxx.xxx/update.xml");
```