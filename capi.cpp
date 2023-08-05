#include <stdint.h>

extern "C" int32_t get_number(void) {
	return 23;
}

extern "C" int32_t modify_number(int (*modify)(int number)) {
	return modify(get_number());
}