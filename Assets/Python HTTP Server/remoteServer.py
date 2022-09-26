# -*- coding: utf-8 -*-
"""
Spyder Editor

This is a temporary script file.
"""

from http.server import BaseHTTPRequestHandler, HTTPServer
import logging

class S(BaseHTTPRequestHandler):
    def _set_response(self):
        self.send_response(200)
        self.send_header('Content-type', 'text/html')
        self.end_headers()

    def do_GET(self):
        logging.info("GET request,\nPath: %s\nHeaders:\n%s\n", str(self.path), str(self.headers))
        self._set_response()
        self.wfile.write("GET request for {}".format(self.path).encode('utf-8'))

    def do_POST(self):
        content_length = int(self.headers['Content-Length']) # <--- Gets the size of data
        post_data = self.rfile.read(content_length) # <--- Gets the data itself
        logging.info("POST request,\nPath: %s\nHeaders:\n%s\n\nBody:\n%s\n",
                str(self.path), str(self.headers), post_data.decode('utf-8'))

        get_Data(post_data)
        self._set_response()
        self.wfile.write("POST request for {}".format(self.path).encode('utf-8'))

def get_Data(post_data):
    data_list = str(post_data).split("&")
    #print(data_list)
    stickX = int(data_list[0][9:])/10000000
    stickY = int(data_list[1][7:])/10000000
    botaoY = bool(int(data_list[2][7:]))
    botaoX = bool(int(data_list[3][7:]))
    botaoA = bool(int(data_list[4][7:]))
    botaoB = bool(int(data_list[5][7:-1]))
    make_something_with_data(stickX,stickY,botaoY,botaoX,botaoA,botaoB)

def make_something_with_data(stickX,stickY,botaoY,botaoX,botaoA,botaoB):
    print("O valor do StickX é: " + str(stickX))
    print("O valor do StickY é: " + str(stickY))
    print("O valor do BotaoY é: " + str(botaoY))
    print("O valor do BotaoX é: " + str(botaoX))
    print("O valor do BotaoA é: " + str(botaoA))
    print("O valor do BotaoB é: " + str(botaoB))
    
    
def run(server_class=HTTPServer, handler_class=S, port=8080):
    logging.basicConfig(level=logging.INFO)
    server_address = ('', port)
    httpd = server_class(server_address, handler_class)
    logging.info('Starting httpd...\n')
    try:
        httpd.serve_forever()
    except KeyboardInterrupt:
        pass
    httpd.server_close()
    logging.info('Stopping httpd...\n')

if __name__ == '__main__':
    from sys import argv

    if len(argv) == 2:
        run(port=int(argv[1]))
    else:
        run()