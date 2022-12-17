// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.Collections.Generic;

public class TheNuckToolbucketTarget : TargetRules
{
	public TheNuckToolbucketTarget(TargetInfo Target) : base(Target)
	{
		Type = TargetType.Game;
		ExtraModuleNames.Add("TheNuckToolbucket");
	}
}
