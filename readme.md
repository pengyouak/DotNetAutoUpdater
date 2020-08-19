### 自动更新组件

---
#### 截图

![image](https://s1.ax1x.com/2020/08/18/dKWSE9.png)

![image](https://s1.ax1x.com/2020/08/19/dQEfdf.png)

![image](https://s1.ax1x.com/2020/08/19/dQE5FS.png)

#### XML 配置

```
<?xml version="1.0" encoding="utf-8"?>
<UpdateOption xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Version>1.1.1.1</Version>
  <ServerUrl>http://xxx.xxx.xxx/</ServerUrl>
  <UpdateMode>2</UpdateMode>
  <ChangeLog></ChangeLog>
  <UpdateItems>
    <UpdateItem>
      <Version>1.0.0.0</Version>
      <Path>DotNetAutoUpdater.dll</Path>
    </UpdateItem>
    </UpdateItem>
    <UpdateItem>
      <Version>1.0.0.0</Version>
      <Path>b/DotNetAutoUpdaterTest.exe</Path>
    </UpdateItem>
    </UpdateItem>
  </UpdateItems>
</UpdateOption>
```

UpdateMode
- 0: 提示更新
- 1: 提示并显示更新详情
- 2: 强制更新

#### 使用

```
new AutoUpdate().Update("http://xxx.xxx.xxx/update.xml");
```