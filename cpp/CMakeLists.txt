cmake_minimum_required(VERSION 3.17)

set(PROJECT_NAME Platform.Timestamps)
project(${PROJECT_NAME})

add_subdirectory(${PROJECT_NAME})

macro(INCLUDE_EXTENSION EXT_NAME)
    add_subdirectory(${PROJECT_NAME}.${EXT_NAME})
    target_link_libraries(${PROJECT_NAME}.${EXT_NAME} PUBLIC ${PROJECT_NAME}.Library)
endmacro()

INCLUDE_EXTENSION(Tests)
