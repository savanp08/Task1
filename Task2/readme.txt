build commands used:

Used docker to simulate server and client

sudo docker pull eghabash/dfly
sudo docker run --cap-add=NET_ADMIN --privileged  --device /dev/net/tun:/dev/net/tun -i -t eghabash/dfly
cd home/dfly && ./bash.sh


**The video playback could not be simulated due to hardware related issues (and docker) but the server ran along with client and logs were generated as mentioned in the code and the logs seemed error free

MakeFile_Ubuntu
# # Compiler and linker settings
# CXX = g++
# CXXFLAGS = -std=c++14 -D__GXX_EXPERIMENTAL_CXX0X__ -O0 -g -Wall
# LDFLAGS = -lpthread -ldl -lboost_regex -lboost_system -ldouble-conversion -lglog -lgflags -lavutil -lavformat -lavcodec -lswscale -lfmt -lfolly

# # Include and library directories
# INCLUDES = -I../third-party-lib/folly-2021.03.15.00 -I../third-party-lib/fmt/include
# LFLAGS = -L../third-party-lib/folly-2021.03.15.00/build/lib -L../third-party-lib/fmt/build

# # Source files and objects
# SRCS = $(wildcard ./src/*.cpp)
# OBJS = $(SRCS:./src/%.cpp=./build/%.o)

# # Targets
# all: client server

# client: $(OBJS)
# 	$(CXX) -o ./build/client $(OBJS) $(LDFLAGS) $(LFLAGS)

# server: Server.o
# 	$(CXX) -o ./build/server ./build/Server.o $(LDFLAGS) $(LFLAGS)

# # To compile individual object files
# ./build/%.o: ./src/%.cpp
# 	$(CXX) $(CXXFLAGS) $(INCLUDES) -c $< -o $@

# # Clean
# clean:
# 	rm -f ./build/*.o ./build/client ./build/server

# # Phony targets
# .PHONY: all client server clean


Evalauted the project using small datasets
