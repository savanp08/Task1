This project has a client which is a simple unity VR video player and a server(nodejs)and the videos have already been preprocessed to serve the client in form of a .mpd file.

Client:

->Open the unity_videoPlayer in unity and start the project and Click on play to run the scene(sampleScene).
->The project has XR plugin and oculus plugin.
->It uses JPEG-DASH protocol to stream the video file and 2 logging scripts have been used to log the deails of the video playback.
->The log files are in Assets are are AdvancedVideoMetricsLogs.txt and VidoPlayerMetricsLog.txt
->logger.cs: It logs essential information like throughput, delay etc while the second script logs additional information related to the video playaback 
like resolution, frameRate, minimum and Maximum time to load frames, etc
->third party package used : AVPro
A trial version of AVPro media player component has been used which has support for HLS and JPEG-DASH streaming.
The url used for fetching the output.mpd file from server is http://localhost:3000/output.mpd

Server

The server is a simple nodejs express server which serves static files that heps the client to stream them.
The video files provided have been processed and converted to segments and metadata to serve the client.
command used : ffmpeg -i input_144p.mp4 -i input_360p.mp4 -i input_720p.mp4 \
-map 0:v -map 1:v -map 2:v -map 0:a \
-b:v:0 100k -s:v:0 256x144 \
-b:v:1 400k -s:v:1 640x360 \
-b:v:2 1500k -s:v:2 1280x720 \
-c:v libx264 -c:a aac -b:a 128k \
-use_timeline 1 -use_template 1 \
-adaptation_sets "id=0,streams=v id=1,streams=a" \
-f dash output.mpd

to run server, open terminal in direcory server and run :
npm install && npm start

This will start a server on port 3000 in the local machine.
