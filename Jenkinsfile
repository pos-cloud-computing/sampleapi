pipeline {
    agent any

    stages {
        stage ('Build Docker image') {
            steps {
                script {
                    dockerapp = docker.build("995396735443.dkr.ecr.us-east-1.amazonaws.com/sampleapi:${env.BUILD_ID}", '-f Web.API\Dockerfile  .')
                }
            }
        }
   }
}