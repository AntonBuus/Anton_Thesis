# Anton Thesis
-----------Project Developers:-----------

Anton Buus Hansen: antonbuushansen@gmail.com


Note that this is a forked repo from a previous project I did in the fall of 2024. The reason for the fork is to reuse some of the visual material for this project, as well as some of the interactions.

In August 2022 the EU commission published Annex1, a set of guidelines and rules for the manufacturing of sterile medicinal products within EU countries. This project aims to help entry-level employees in the least critical clean rooms to enhance their knowledge about the new regulations learned through reading them. To do so, a Virtual Reality application utilizing gamification and adaptability was developed. This is the source code of the developed prototype.

The project is made with Meta Quest2 in mind and cannot be played without it, though it might be forward compatible with Quest 3.

This project uses assets from the _Chemistry Lab Items Pack_ from the Unity Asset store:
https://assetstore.unity.com/packages/3d/environments/chemistry-lab-items-pack-220212

The project uses the "Simple rubber conveyor" 3D model as a basemesh for our own conveyor belt. Remodeling and textures are done by us.
https://sketchfab.com/3d-models/simple-rubber-conveyor-0819b51c59c3407cb98f0e2c75029e30

The project also uses this clipboard as a basemesh for our clipboard. Textures are done by us.
https://sketchfab.com/3d-models/clipboard-7mb-a5f71d0bd08d4653880a92f19dbbf72a

The mask for the shader graph was developed based on the _Unity How to create Shader Graph - Simple black & white image alpha cut (make your life easier)_ tutorial by _Tannos 2023_:
https://www.youtube.com/watch?v=QUQijOdVfrY&ab_channel=Tannos2023

To implement Machine Learning in this project we have used an external package called InteractML which provides node-based programming directly in the Unity editor: https://github.com/Interactml/iml-unity We specifically used the downloadable version here: https://github.com/Interactml/iml-unity/releases

If you are getting  compiler errors from a .dll-file you might be able to fix the issue here: https://github.com/Interactml/iml-unity/wiki/Fixing-Error-DllNotFoundException:-RapilibPlugin
