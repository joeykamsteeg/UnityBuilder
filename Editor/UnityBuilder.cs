using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

namespace UnityBuilder {

	public class UnityBuilder : EditorWindow {

		private bool buildWindows = false;
		private bool buildMac = false;
		private bool buildiOS = false;
		private bool buildAndroid = false;
		private bool buildWebGL = false;

		private string buildLocation = "";

		[MenuItem("Window/Unity Builder")]
		public static void Init(){
			Debug.Log("Showing Unity Builder");
			UnityBuilder builder = CreateInstance<UnityBuilder>();
			builder.titleContent.text = "UnityBuilder";
			builder.Show();
		}

		private void GetScenes(){
			
		}

		public void OnGUI(){
			GUILayout.Label("Select the following platforms below for\nwhich platforms you want to build the Unity project");
			GUILayout.Space(10);

			GUILayout.Label("Build Path: " + this.buildLocation );
			if( GUILayout.Button("Select Build Location") ){
				this.buildLocation = EditorUtility.OpenFolderPanel( "Select Build Path", "", "" );
			}

			GUILayout.Space(10);
			GUILayout.BeginVertical();

			GUILayout.Label("Platforms:");
			this.buildWindows = EditorGUILayout.BeginToggleGroup("Window", this.buildWindows ); EditorGUILayout.EndToggleGroup();
			this.buildMac = EditorGUILayout.BeginToggleGroup("MacOS", this.buildMac ); EditorGUILayout.EndToggleGroup();
			this.buildiOS = EditorGUILayout.BeginToggleGroup("iOS", this.buildiOS ); EditorGUILayout.EndToggleGroup();
			this.buildAndroid = EditorGUILayout.BeginToggleGroup("Android", this.buildAndroid ); EditorGUILayout.EndToggleGroup();
			this.buildWebGL = EditorGUILayout.BeginToggleGroup("WebGL", this.buildWebGL ); EditorGUILayout.EndToggleGroup();

			GUILayout.EndVertical();

			GUILayout.Space(20);

			if( GUILayout.Button("Build")){
				String date = DateTime.Now.ToString("yyyy-m-d hh-mm-ss");

				if( this.buildWindows == true ){
					this.Build( new BuilderTarget(BuildTarget.StandaloneWindows, "Windows64", ".exe"), date );
				}

				if( this.buildMac == true ){
					this.Build(new BuilderTarget(BuildTarget.StandaloneOSXIntel64, "MacOS"), date);
				}

				if( this.buildWebGL == true ){
					this.Build( new BuilderTarget(BuildTarget.WebGL, "WebGL" ), date );
				}

				if( this.buildiOS == true ){
					this.Build( new BuilderTarget(BuildTarget.iOS, "iOS"), date );
				}

				if( this.buildAndroid == true ){
					this.Build( new BuilderTarget(BuildTarget.Android, "Android" ), date );
				}
			}
		}

		private void Build( BuilderTarget target, string date ){
			if( this.buildLocation.Length == 0 ){
				Debug.LogError("UnityBuilder: No Build Location given");
				return;
			}

			EditorBuildSettingsScene[] scenes = UnityEditor.EditorBuildSettings.scenes;
			string[] sceneNames = new string[ scenes.Length ];

			for( int i = 0; i < scenes.Length; i++){
				EditorBuildSettingsScene scene = scenes[i];

				string sceneName = scene.path;
				sceneNames[i] = sceneName;
			}

			BuildPipeline.BuildPlayer( sceneNames, this.buildLocation + "/" + date +  "/" + target.folder + "/PlayerBuild" + target.extension, target.target, BuildOptions.None );
		}
	}
}