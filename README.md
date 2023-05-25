# ICSM_VN_pulic
It is the software supporting soil classification of Vietnam
They include three parts: (1) files related to codes,
(2) 13 files to test the software (NhanDienMDGuiTapChi/Test), (3) Setup file. The test files are BTGD_04.ndmd_tri, BTGD_05.ndmd_tri,
BTGD_06.ndmd_tri, BTGD_07.ndmd_tri, BTGD_08.ndmd_tri, VN05.ndmd_tri, VN14.ndmd_tri, VN34.ndmd_tri, VN37.ndmd_tri,
VN46.ndmd_tri, VN47.ndmd_tri, VN54.ndmd_tri, VN60.ndmd_tri. The setup file is ICSM_1.1_setup.

Why and how to use the code.
We choose the windform for our program because it provides one of the most productive ways to create desktop apps based on the visual designer provided in Visual Studio. Functionality such as drag-and-drop placement of visual controls makes it easy to build desktop apps. 
The software was coded by the language of VB.net because it is suitable to build a windows form desktop app. 
We used the NET framework 4.5 because it has been integrated in the Windows 10 which is the most popular in Vietnam, now.
The Visual Studio 2013 was used because it is a software supporting well for programing, and suitable for the language of VB.net. We used the version because we had its licience.
The DXperience13 is a component supporting well to code based on the visual studio. We also had its licience.

The software was programed by using the language of VB.net based on the software of Visual Studio 2013, net framework 4.5, and using the tool set of Devexpress.net (DXperience13).
In order to test code, you have to 
I. set up:
1. Visual Studio 2013
2. Devexpress.net (DXperience13
II. Open Visual Studio (VS), from VS, open solution NhanDienMauDat.sln and pressing F5 to run debug for the solution

- To launch the test the software, please open folder bin\Debug and run file NhanDienMauDat with icon (a leaf shape and
green). If you run a antivirus software, a question ask, please click ok.
- After starting successfully, please open a file by click icon with folder shape and select one of the test files in the folder “Test”.
- You can move to names of horizons in the first block (from the left to right) which mention in the fourth block. You can see data that
input to determine the horizon. Please click mouse on one textbox, and put Enter. Consequently, a warning will appear to affirm that “It
is true for the horizon” or “It is false for the horizon” or “It is not enough data to affirm”. You can do the same for the second and third
block if you see they appear in the fourth block. 
- Next, please click the tab ”Nhóm đất”, click mouse on one textbox, and put Enter. It show “It is true for the soil group” or “It is false for
the soil group” or “It is not enough data to affirm”. You can do the same for the ‘Loại đất”, “Phụ loại đất”. In the last tab, please input a value
into a blank textbox and put Enter. The final result shows what it is a soil variety.
