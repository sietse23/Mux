//AD  <- Needed to identify //
//--automatically built--


adm.loadVideo("$PAD");
adm.videoCodec("xvid4", "params=2PASSBITRATE=3000", "profile=244", "rdMode=3", "motionEstimation=3", "cqmMode=0", "arMode=1", "maxBFrame=2", "maxKeyFrameInterval=20", "nbThreads=99", "qMin=2", "qMax=2", "rdOnBFrame=True", "hqAcPred=True", "optimizeChrome=True", "trellis=True");
adm.addVideoFilter("swscale", "width=624", "height=352", "algo=2", "sourceAR=1", "targetAR=1");
adm.audioCodec(0, "LavAC3", "bitrate=384");
adm.setContainer("AVI", "odmlType=1");
