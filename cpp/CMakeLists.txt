cmake_minimum_required(VERSION 3.17)

set(PROJECT_NAME Platform.Timestamps)
project(${PROJECT_NAME})

add_subdirectory(${PROJECT_NAME})

macro(INCLUDE_EXTENSION EXTENSION_NAME)
    add_subdirectory(${PROJECT_NAME}.${EXTENSION_NAME})
    target_link_libraries(${PROJECT_NAME}.${EXTENSION_NAME} PUBLIC ${PROJECT_NAME}.Library)
endmacro()

INCLUDE_EXTENSION(Tests)