FROM alpine:3.21.0
RUN ls -laR . > localDirs.txt
RUN ls -laR / > rootDirs.txt
RUN echo "---------------------"
RUN echo "--- LocalDirs ---"
RUN cat localDirs.txt
RUN echo "---------------------"
RUN echo "--- RootDirs ---"
RUN cat rootDirs.txt