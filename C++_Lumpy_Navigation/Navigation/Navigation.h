#ifndef NAVIGATION_H
#define NAVIGATION_H

typedef struct _XYZ { float X; float Y; float Z; } XYZ;

class Navigation
{
public:
	static const int ERROR = -1;
	static const int ERROR_LOAD_MAP = -2;
	static const int ERROR_PATH_CALCULATION = -3;

public:
	static Navigation* GetInstance();
	void Initialize();
	void Release();
	XYZ* CalculatePath(unsigned int mapId, XYZ start, XYZ end, bool useStraightPath, int* parLength);
	void GetPath(XYZ* path, int length);
	void FreePathArr(XYZ* pathArr);

private:
	static Navigation* s_singletonInstance;
	unsigned int lastMapId;
	XYZ* currentPath;
};

#endif