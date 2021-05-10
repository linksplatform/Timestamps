project(${PROJECT_NAME})

include(FetchContent)

macro(fetch_package package_name)
    FetchContent_MakeAvailable(googletest)

    FetchContent_GetProperties(googletest)
    if(NOT googletest_POPULATED)
        FetchContent_Populate(googletest)
        add_subdirectory(${googletest_SOURCE_DIR} ${googletest_BINARY_DIR})
    endif()
endmacro()

FetchContent_Declare(
        googletest
        URL https://github.com/google/googletest/archive/refs/tags/release-1.10.0.zip
)

set(gtest_force_shared_crt ON CACHE BOOL "" FORCE)

FetchContent_MakeAvailable(googletest)
FetchContent_GetProperties(googletest)
if(NOT googletest_POPULATED)
    FetchContent_Populate(googletest)
    add_subdirectory(${googletest_SOURCE_DIR} ${googletest_BINARY_DIR})
endif()

add_executable(${PROJECT_NAME}.Tests ${PROJECT_NAME}.Tests.cpp)
set_target_properties(${PROJECT_NAME}.Tests PROPERTIES CXX_STANDARD 20)
target_link_libraries(${PROJECT_NAME}.Tests PUBLIC gtest gtest_main)