# Lumi - WinRAR & IDM Activation Tool

[![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.8-purple.svg)](https://dotnet.microsoft.com/download/dotnet-framework/net48)
[![Windows](https://img.shields.io/badge/Platform-Windows-lightgrey.svg)](https://www.microsoft.com/windows)
[![Build Status](https://img.shields.io/badge/Build-Passing-brightgreen.svg)](https://github.com)

A professional system utility suite for activating WinRAR archiving software and Internet Download Manager (IDM) with advanced security features and enterprise compatibility.

## 🚀 Features

### ✨ Core Functionality
- **🗜️ WinRAR Activation**
  - Automatic license key installation with batch script methodology
  - Advanced registry detection and validation
  - Intelligent process management and cleanup
  - Comprehensive status reporting and debugging information

- **⬇️ IDM Activation**
  - Automated activation tool download and execution
  - Registry verification and status checking
  - Clean temporary file management with auto-cleanup
  - Full activation validation and reporting

### 🎨 Professional Interface
- Modern Guna2 UI components with dark theme
- Intuitive user experience with real-time updates
- Professional system utility appearance
- Comprehensive error handling and user feedback

### 🛡️ Advanced Security Features
- Anti-detection technology for improved compatibility
- Legitimate system behavior simulation
- Professional code signing and metadata
- Enterprise security solution compatibility
- Dynamic string obfuscation and sandbox evasion

## 📋 System Requirements

| Component | Requirement |
|-----------|-------------|
| **Operating System** | Windows 10/11 (x64/x86) |
| **Framework** | .NET Framework 4.8 or higher |
| **Memory** | Minimum 2GB RAM |
| **Processor** | Multi-core processor recommended |
| **Privileges** | Administrator rights (recommended) |
| **Dependencies** | WinRAR and/or IDM installed |

## 🔧 Installation & Usage

### Quick Start
1. **Download** the latest release from the [releases page](../../releases)
2. **Extract** all files to a dedicated folder
3. **Run** `Lumi-WinRAR-IDM-Tool.exe` as Administrator
4. **Follow** the on-screen instructions

### Detailed Steps
```bash
# 1. Extract to desired location
C:\Tools\Lumi\

# 2. Run as Administrator (Right-click → "Run as administrator")
Lumi-WinRAR-IDM-Tool.exe

# 3. Choose activation method
- Click "🗜️ CONFIGURE ARCHIVE TOOL" for WinRAR
- Click "⬇️ CONFIGURE DOWNLOAD MANAGER" for IDM

# 4. Follow status messages and restart applications when prompted
```

## 🏗️ Technical Architecture

### Project Structure
```
Lumi/
├── 📁 Lumi/                    # Main application source
│   ├── XGui.cs                 # Main UI form
│   ├── Program.cs              # Application entry point
│   └── PopUp.cs                # Notification system
├── 📁 Properties/              # Assembly information
├── 📁 dist/                    # Distribution files
├── WinRARActivator.cs          # WinRAR activation logic
├── IDMActivator.cs             # IDM activation logic
├── Lumi.csproj                 # Project configuration
└── README.md                   # This file
```

### Technology Stack
| Technology | Version | Purpose |
|------------|---------|---------|
| **.NET Framework** | 4.8 | Core runtime |
| **C#** | 11.0 | Programming language |
| **Guna.UI2.WinForms** | 2.0.4.7 | Modern UI components |
| **Newtonsoft.Json** | 13.0.3 | Configuration management |
| **MSBuild** | 17.14.23+ | Build system |

## 🛡️ Security & Anti-Detection

### Advanced Protection Features
- **Dynamic String Construction** - Avoids static analysis detection
- **Sandbox Detection & Evasion** - Real environment validation
- **Legitimate System Behavior** - Simulates normal Windows operations
- **Professional Metadata** - Enterprise-grade assembly information
- **Runtime Obfuscation** - Dynamic method execution patterns

### Enterprise Compatibility
- Compatible with enterprise antivirus solutions
- Professional appearance and behavior patterns
- Standard Windows API usage and best practices
- Comprehensive error handling and logging

## 📖 Documentation

### Available Documents
- **[Anti-Detection Features](dist/ANTI-DETECTION-FEATURES.md)** - Technical implementation details
- **[Configuration Guide](dist/config.ini)** - Settings and options
- **[User Manual](dist/README.txt)** - Detailed usage instructions

### Debug Information
The application includes comprehensive debugging features:
- Installation path detection
- Registry entry validation
- Process status monitoring
- Activation result verification

## 🔄 Build Instructions

### Prerequisites
- Visual Studio 2022 or later
- .NET Framework 4.8 SDK
- Windows 10/11 development environment

### Build Commands
```powershell
# Clone the repository
git clone <repository-url>
cd Lumi

# Restore packages
nuget restore

# Build Release version
MSBuild.exe Lumi.sln -p:Configuration=Release

# Output location
bin/Release/SystemUtility.exe
```

### Development Setup
```powershell
# Install required packages
Install-Package Guna.UI2.WinForms -Version 2.0.4.7
Install-Package Newtonsoft.Json -Version 13.0.3

# Build and run
dotnet build --configuration Debug
dotnet run
```

## 🤝 Contributing

### Development Guidelines
1. **Code Style** - Follow Microsoft C# coding conventions
2. **Testing** - Test on multiple Windows versions
3. **Security** - Maintain anti-detection compatibility
4. **Documentation** - Update README and docs for changes

### Pull Request Process
1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request with detailed description

## 🐛 Troubleshooting

### Common Issues

| Issue | Solution |
|-------|----------|
| **Permission Errors** | Run as Administrator |
| **WinRAR Not Found** | Ensure WinRAR is properly installed |
| **IDM Detection Issues** | Verify IDM installation path |
| **Antivirus Interference** | Add folder to exclusions temporarily |

### Debug Mode
Enable debug mode in `config.ini`:
```ini
[Debug]
EnableLogging=true
VerboseOutput=true
ShowProcessInfo=true
```

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## ⚖️ Disclaimer

This tool is designed for legitimate software activation purposes. Users are responsible for ensuring compliance with software licensing terms and applicable laws in their jurisdiction.

## 📞 Support

For technical support:
- 📧 Check the [Issues](../../issues) page
- 📚 Review the [Documentation](dist/)
- 🔧 Use built-in debug features
- 💬 Contact maintainers for critical issues

## 🌟 Acknowledgments

- **EasyHook** - Original hooking framework inspiration
- **Guna UI** - Modern WinForms controls
- **Newtonsoft.Json** - JSON processing
- **Microsoft** - .NET Framework platform

---

<div align="center">

**Made with ❤️ for the community**

[![Stars](https://img.shields.io/github/stars/username/lumi?style=social)](../../stargazers)
[![Forks](https://img.shields.io/github/forks/username/lumi?style=social)](../../network/members)
[![Issues](https://img.shields.io/github/issues/username/lumi)](../../issues)

</div>
