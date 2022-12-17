// Copyright 1998-2019 Epic Games, Inc. All Rights Reserved.

#include "TheNuckToolbucket.h"
#include "Interfaces/IPluginManager.h"
#include "Logging/LogMacros.h"
#include "Misc/Paths.h"


void FTheNuckToolbucket::StartupModule()
{
#if (ENGINE_MINOR_VERSION >= 21)
	FString ShaderDirectory = FPaths::Combine(FPaths::ProjectDir(), TEXT("Shaders"));
	AddShaderSourceDirectoryMapping("/Project", ShaderDirectory);
#endif
}

void FTheNuckToolbucket::ShutdownModule()
{
}

IMPLEMENT_PRIMARY_GAME_MODULE( FTheNuckToolbucket, TheNuckToolbucket, "TheNuckToolbucket" );
 