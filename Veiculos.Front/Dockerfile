FROM node:20
WORKDIR /app-front
COPY . .

RUN npm init
RUN npm install express
RUN npm	install @angular/cli -g
EXPOSE 4200
CMD ["ng", "serve", "--host", "0.0.0.0"]