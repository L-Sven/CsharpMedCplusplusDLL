#include "stdafx.h"
#include "ClassLib.h"

//Wrapper funktion för klassen Test. Om nya methods läggs till till Test måste de också läggas till här.

extern "C" __declspec(dllexport)  ClassLib::Test* CreateTestClass()
{
	return new ClassLib::Test();
}

extern "C" __declspec(dllexport) void DisposeTestClass(ClassLib::Test* pObject)
{
	if (pObject != nullptr)
	{
		delete pObject;
		pObject = nullptr;
	}
}

extern "C" __declspec(dllexport) int Add(ClassLib::Test* pObject, int* a, int* b)
{
	if (pObject != nullptr)
	{
		return pObject->Add(a, b);
	}
	return 0;
}
