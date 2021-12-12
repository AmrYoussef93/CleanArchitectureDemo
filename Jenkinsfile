pipeline {
  agent any
  stages {
    stage("build") {
      steps {
        echo 'retoring packages'
        sh 'dotnet restore'
        echo 'build app'
        sh 'dotnet build --not-restore'
      }
    }
    
    stage("test") {
      steps {
          echo 'testing packages'
      }
    }
    
    stage("deploy") {
      steps {
        echo 'deploying packages'
      }
    }
  }
}
