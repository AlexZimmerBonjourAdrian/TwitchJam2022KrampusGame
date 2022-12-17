// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;

public class TheNuckToolbucket : ModuleRules
{
	public TheNuckToolbucket(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicDependencyModuleNames.AddRange(new string[] { "RHI", "RenderCore", "Core", "CoreUObject", "Engine", "InputCore", "HeadMountedDisplay" });
	}
}
