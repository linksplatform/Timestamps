add_library(${PROJECT_NAME}.Library INTERFACE)

target_include_directories(${PROJECT_NAME}.Library INTERFACE ${CMAKE_CURRENT_SOURCE_DIR})

set_target_properties(${PROJECT_NAME}.Library PROPERTIES CXX_STANDARD 20)
