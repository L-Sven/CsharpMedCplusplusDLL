#include "stdafx.h"

#include "ClassLib.h"

int ClassLib::Test::Add(int* a, int* b)
{
	int c = *a;
	int d = *b;
	return *a + *b;
}

int ClassLib::Test::Subtract(int* a, int* b)
{
	return *a - *b;
}
