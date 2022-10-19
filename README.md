# Download
Go to [releases](https://github.com/Gaggol/AzurLaneDatabase/releases), pick either self-contained Win x64 or Portable.

Self-Contained can be run without any other delays.
Portable requires .NET 6.0 Runtime.

# Usage
- Left Click on ship to mark them as owned.
- Right Click to mark them as unowned.
- Ctrl+Left Click to mark as owned, plus max stars.
- Shift+Left Click to try to retrofit ships.
- Ctrl+Shift+Left Click, mark as owned, try to retrofit, max stars.
- Left Click on stars to increase by one.
- Right Click on stars to decrease by one.
- Ctrl+S to save.

Search terms available: owned, missing, lb, retrofit, retrofitted

ASCII-Conversion is implemented.

Example: Köln can be searched as either Köln, or Koln.

|owned|missing|lb|retrofit|retrofitted|
|---|---|---|---|---|
|Show ships marked as owned|Show ships NOT marked as owned|Show ships with max stars|Show potential retrofits|Show only retrofitted ships|

# Development

Required images for characters available [here](https://azurlane.koumakan.jp/wiki/List_of_Ships_by_Image).

File extension required is .png

Place images in /Resources/chars/

File name need to fit database name, _Retrofit is appended for retrofits.

Example: Abukuma.png, Abukuma_Retrofit.png

Database pre-built with only Normal & Retrofitted ships. Bulins not included.

Two versions of database formats supported, example below.


| Name | Rarity | Stars | Is Owned | Can be Retrofit | Retrofitted |
|---|---|---|---|---|---|
|Abercrombie|2|2|0|false|false|


| Name | Rarity | Stars | Is Owned | Can be Retrofit | Retrofitted | V:2 |
|---|---|---|---|---|---|---|
|Abercrombie|2|2|False|False|False|
