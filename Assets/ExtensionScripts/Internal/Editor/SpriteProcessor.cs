//using UnityEditor;
//using UnityEngine;

//public class SpriteProcessor : AssetPostprocessor
//{
//    private void OnPostprocessTexture(Texture2D _texture)
//    {
//        string lowerCaseAssetPath = assetPath.ToLower();

//        TextureImporter textureImporter = (TextureImporter)assetImporter;

//        if(lowerCaseAssetPath.IndexOf("/sprites/") != -1)
//        {
//            textureImporter.textureType = TextureImporterType.Sprite;
//            textureImporter.textureCompression = TextureImporterCompression.CompressedHQ;
//        }
//        else if(lowerCaseAssetPath.IndexOf("/gui/") != -1)
//        {
//            textureImporter.textureType = TextureImporterType.Default;
//            textureImporter.textureCompression = TextureImporterCompression.CompressedHQ;
//        }
//        else if(lowerCaseAssetPath.IndexOf("/cursor/") != -1)
//        {
//            textureImporter.textureType = TextureImporterType.Cursor;
//            textureImporter.textureCompression = TextureImporterCompression.CompressedHQ;
//        }
//    }
//}
