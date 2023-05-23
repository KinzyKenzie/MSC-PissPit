using UnityEngine;
using MSCLoader;

namespace PissPitMod
{
    public class SaveData
    {
        public double PissPitLevel = 0;
    }

    public class PissPitMod : Mod
    {
        public override string ID => "PissPit";
        public override string Name => "Piss Pit";
        public override string Author => "KinzyKenzie";
        public override string Version => "0.1";
        public override string Description => "Finally makes the Piss Pit more than just pretend!";
        // public override string UpdateLink => "https://www.nexusmods.com/mysummercar/mods/9999";
        // public override byte[] Icon => Properties.Resources.Icon;

        // Learn more at https://mscloaderpro.github.io/docs/!

        //public static readonly Vector3 PissPositionRoot = new Vector3( -1549, 3.2f, 1192 );
        public static readonly Vector3 PissPositionRoot = new Vector3( -15, 0, 0 );
        public static readonly Vector3 PissPlaneSize = new Vector3( 1, 0.4f, 5 );

        public SettingSlider PissLimitSlider;

        private Shader pissShader;
        private Texture2D pissTexture, waterTexture; // TODO: Recolour standard water texture in order to deprecate external texture loading.
        private Material pissMaterial;
        private GameObject pissPlane;
        ///private Texture2D pissBumpmap;

        public double PissLimit { get; set; }
        public double PissLevel { get; set; }

        // Here you can add all the settings you want for your mod!
        public override void ModSettings() {
            PissLimitSlider = modSettings.AddSlider( "pisslimit", "Maximum Piss Height", 700, 0, 1000, ( value ) => { PissLimit = value; } );

            Log( "Loading saved data" );
            SaveData loadedData = ModSave.Load<SaveData>( Name );
            PissLevel = loadedData.PissPitLevel;
        }

        // This is likely the method you want to use to load your stuff in the game.
        public override void OnLoad() {
            Log( "Loading texture assets" );
            pissShader = Shader.Find( "Legacy Shaders/Transparent/Diffuse" );
            pissTexture = ModAssets.LoadTexturePNG( "Mods/Assets/piss512.png" );
            ///pissBumpmap = ModAssets.LoadTexturePNG( "Mods/Assets/piss512_bump.png", true );

            foreach( Texture2D tx in Object.FindObjectsOfType<Texture2D>() ) {
                if( tx.name == "water" )
                    waterTexture = tx;
            }

            // Cannot add Normal Map from code. Must be added through AssetBundle
            if( waterTexture == null ) {
                pissMaterial = new Material( pissShader ) {
                    mainTexture = pissTexture,
                    color = new Color( 1, 1, 1, 0.5f )
                };
            } else {
                pissMaterial = new Material( pissShader ) {
                    mainTexture = waterTexture,
                    color = new Color( 1, 1, 0, 0.5f )
                };
            }

            Log( "Creating piss plane" );
            pissPlane = PissHelper.CreatePlane( Name, PissPlaneSize, pissMaterial );
            pissPlane.transform.position = PissPositionRoot;
        }

        // If the player starts a new game, there are things you need to reset for maximum immersion in the game.
        public override void OnNewGame() {
            ModSave.Delete( Name );
            PissLevel = 0;
        }

        // If you have something to save, this would be the place to do it!
        public override void OnSave() {
            ModSave.Save( Name, new SaveData {
                PissPitLevel = PissLevel
            } );
        }

        private void Log( string text ) {
            ModConsole.Log( "[PISS] " + text );
        }
    }
}
