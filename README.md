# ColTools
**Color tools library for C#**

**Current version**: v0.2-beta
- - -
### Info
ColTools is a C# library for basic color manipulation.

Currently, it supports:

* Generation of colors (HEX, RGB, HSL, CMYK, HSV)
* Basic shade detection of colors (only HEX and doesn't always work)
* Color format conversion:
    * RGB <> HEX
    * RGB <> HSL
    * HEX <> HSL

### Documentation
Documentation for 0.2 is available [here](https://github.com/cernysv/ColTools/wiki/Documentation-for-0.2).  
Older versions are available [here](https://github.com/cernysv/ColTools/wiki/Documentation-for-older-versions).  
[Installation](https://github.com/cernysv/ColTools/wiki)

### Changelog 0.2
* Added support more color formats for generators
* Modified the shade detector, but sometimes it doesn't detect the color
* Improved used names (rVm > redValueMin, hC > hexColor etc)
* Changed licensing from custom to BSD 2-Clause
* Renamed RandomColorPalette() to MultipleRandomColors()

### Todo 0.3
* Fix the shade detector and add support for RGB
* Add conversion support for more formats
* Overall code clarity fix
* Complete color inversion for HEX and RGB
* Change the output format of MultipleRandomColors() to a string array

### Releases
[0.2](https://github.com/cernysv/ColTools/releases/v0.2-beta)

### License
This project is licensed under the [BSD 2-Clause License](https://opensource.org/license/bsd-2-clause).  
Additionally, standard .NET libraries (System.*) are used that are made by Microsoft and are licensed under the [MIT License](https://opensource.org/license/mit).

### Contact
For any questions or issues, contact me at [cernysv@outlook.com](mailto:cernysv@outlook.com) or create an [issue](https://github.com/cernysv/ColTools/issues/new/choose).
</br>
</br>
</br>
</br>
</br>
<sub>0.3 soon™</sub>
