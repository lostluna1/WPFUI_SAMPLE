# WPFUI_SAMPLE

这是一个基于 WPF 和 .NET 8 的示例项目，展示了如何使用 MVVM 模式和依赖注入来构建现代桌面应用程序。

## 项目结构

WPFUI_SAMPLE
├── Assets
│   └── wpfui-icon-256.png
|   Contracts
|   ├── Services(接口定义)
├── Views
│   └── Windows
│   |    └── MainWindow.xaml
|   Services(接口实现)
├── ViewModels
│   └── MainWindowViewModel.cs
├── Models
│   └── (模型文件)
├── App.xaml
├── App.xaml.cs
├── MainWindow.xaml.cs
└── (其他项目文件)
### 文件夹和文件说明

- **Assets**: 存放项目中使用的资源文件，如图标、图片等。
  - `wpfui-icon-256.png`: 应用程序图标文件。

- **Views**: 存放视图文件，按照 MVVM 模式，视图文件通常是 XAML 文件。
  - **Windows**: 存放窗口视图文件。
    - `MainWindow.xaml`: 主窗口的 XAML 文件，定义了窗口的布局和 UI 元素。

- **ViewModels**: 存放视图模型文件，负责处理视图的逻辑和数据绑定。
  - `MainWindowViewModel.cs`: `MainWindow.xaml` 对应的视图模型文件，包含窗口的逻辑处理和数据绑定。

- **Models**: 存放模型文件，定义应用程序的数据结构。
  - `AppConfig.cs`: 应用程序配置模型，包含配置文件夹和属性文件名。

- **App.xaml**: 应用程序的入口文件，定义全局资源和应用程序级别的事件。
- **App.xaml.cs**: `App.xaml` 的后台代码(code-behind)文件，包含依赖注入的配置和应用程序的启动逻辑。

- **WPFUI_SAMPLE.csproj**: 项目的配置文件，定义了项目的目标框架、依赖项和其他设置。
