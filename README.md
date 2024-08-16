# MasterTools - Table importer for Unity

[![Release](https://img.shields.io/github/v/release/mackysoft/MasterTools)](https://github.com/mackysoft/MasterTools/releases) [![openupm](https://img.shields.io/npm/v/com.mackysoft.mastertools?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.mackysoft.mastertools/)

**Created by Hiroya Aramaki ([Makihiro](https://twitter.com/makihiro_dev))**

## What is MasterTools?

![](https://github.com/mackysoft/MasterTools/blob/master/Documentation/Toolbar.gif?raw=true)

MasterTools is a table importer for Unity that allows you to import tables from Excel files and generate master data.

This library is a middle layer tool, extended by default with [MessagePack](https://github.com/MessagePack-CSharp/MessagePack-CSharp), [MasterMemory](https://github.com/Cysharp/MasterMemory), [NPOI](https://github.com/nissl-lab/npoi). These dependencies can be replaced by your extensions.

## <a id="index" href="#index"> Table of Contents </a>

- [🔰 Get started](#get-started)
- [📥 Installation](#installation)
- [✉ Help & Contribute](#help-and-contribute)
- [📔 Author Info](#author-info)
- [📜 License](#license)

## <a id="get-started" href="#get-started"> 🔰 Get started </a>

`ImportTableFrom` attribute is used to specify the table to import. By assigning this attribute to a data type, the type can be marked as the target for importing the table by MasterTools.

In the following example, “root path + `Quest`” is used as the path to import the table of `QuestMasterData`. The root path can be set in the import options.

```cs
using UnityEditor;
using MessagePack;
using MackySoft.MasterTools;

[ImportTableFrom("Quest")]
[MemoryTable("Quest")]
[MessagePackObject]
public sealed class QuestMasterData
{
    [PrimaryKey]
    [Key(0)]
    public int Id { get; private set; }

    [Key(1)]
    public string Name { get; private set; }

    public QuestMasterData(int Id, string Name)
    {
        this.Id = Id;
        this.Name = Name;
    }
}
```

Next, the MasterTools importer must be set up. Basically, the import pipeline can be set up by initializing `MasterToolsImporter.DefaultOptions`.
In the following code, the `MasterToolsImporter` is initialized during the initialization of the Unity editor.

```cs
using UnityEditor;
using MessagePack;
using MessagePack.Resolvers;
using MackySoft.MasterTools;
using MackySoft.MasterTools.Example.MasterData;
using MackySoft.MasterTools.Example.MasterData.Resolvers;

public static class MasterToolsInitializer
{
    [InitializeOnLoadMethod]
    static void Initialize ()
    {
        MasterToolsImporter.DefaultOptions = new MasterToolsOptions
        {
            DefaultOutputDirectoryPath = "Example/MasterData",
            TablesDirectoryPath = "../../MasterData",
            DefaultSheetName = "Main",
            Processor = MasterBuilderProcessor.Create(ctx =>
            {
                try
                {
                    // Initialize MessagePack
                    StaticCompositeResolver.Instance.Register(
                        MasterMemoryResolver.Instance,
                        GeneratedResolver.Instance,
                        StandardResolver.Instance
                    );
                    var options = MessagePackSerializerOptions.Standard.WithResolver(StaticCompositeResolver.Instance);
                    MessagePackSerializer.DefaultOptions = options;
                }
                catch
                {
                    // Catch and forget.
                }

                return new MasterMemoryDatabaseBuilder("database", new DatabaseBuilder(), x => new MemoryDatabase(x).Validate());
            }),
            TableReader = new XlsxTableReader(),
            JsonDeserializer = new MessagePackJsonDeserializer(),
        };
    }
}
```

In this example, the MasterMemory database is generated by searching for tables in the `../../MasterData` directory, relative to `Application.dataPath` (which corresponds to the `Assets/` directory), and outputting the data to `Example/MasterData` directory.

If the `MasterToolsImporter.DefaultOptions` is set, the import process can be executed from the `Tools/Master Tools/Import (with default options)` menu in the Unity editor.

![](https://github.com/mackysoft/MasterTools/blob/master/Documentation/Toolbar.png?raw=true)

Alternatively, import can also be performed by using the `MasterToolsImporter.ImportWithDefaultOptions` or `MasterToolsImporter.Import` functions.

```cs
void MasterToolsImporter.ImportWithDefaultOptions();
void MasterToolsImporter.Import(MasterToolsOptions options);
```

## <a id="installation" href="#installation"> 📥 Installation </a>

### Install dependencies

MasterTools depends on the following packages by default. If you do not customize the MasterTools import pipeline, please install the following packages first.

#### MessagePack and MasterMemory

Download the MessagePack and MasterMemory unitypackages from the releases page and install them in your project.

- [MessagePack](https://github.com/MessagePack-CSharp/MessagePack-CSharp)
- [MasterMemory](https://github.com/Cysharp/MasterMemory)

#### NPOI

Since NPOI is distributed via NuGet, NuGetForUnity must be installed first. In PackageManager, select `Add package from git URL` and enter the following URL.

```
https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity
```

after installing NuGetForUnity, search for and install NPOI.

![](https://github.com/mackysoft/MasterTools/blob/master/Documentation/NPOI.png?raw=true)

### Install MasterTools via PackageManager

After installing the required dependencies, install MasterTools by selecting `Add package from git URL` in PackageManager and entering the following URL.

```
https://github.com/mackysoft/MasterTools.git?path=Unity/Assets/MackySoft/MackySoft.MasterTools
```

### Install MasterTools via .unitypackage

If you do not need any of the above either dependencies, you will need to remove the unwanted integration. In this case, installing via unitypackage is recommended.

Releases: https://github.com/mackysoft/MasterTools/releases

## <a id="help-and-contribute" href="#help-and-contribute"> ✉ Help & Contribute </a>

I welcome feature requests and bug reports in [issues](https://github.com/mackysoft/MasterTools/issues) and [pull requests](https://github.com/mackysoft/MasterTools/pulls).

If you feel that my works are worthwhile, I would greatly appreciate it if you could sponsor me. Private sponsor and one-time donate are also welcome.

GitHub Sponsors: https://github.com/sponsors/mackysoft

## <a id="author-info" href="#author-info"> 📔 Author Info </a>

Hiroya Aramaki is a indie game developer in Japan.

- Twitter: [https://twitter.com/makihiro_dev](https://twitter.com/makihiro_dev)

## <a id="license" href="#license"> 📜 License </a>

This library is under the [MIT License](https://github.com/mackysoft/Navigathena/blob/main/LICENSE).
